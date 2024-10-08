﻿using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Notification.DTOs;
using Oostel.Application.Modules.Notification.Service;
using Oostel.Application.RequestFilters;
using Oostel.Application.UserAccessors;
using Oostel.Common.Constants;
using Oostel.Common.Exceptions;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.Media;
using Oostel.Infrastructure.Repositories;
using System.ComponentModel.Design;

namespace Oostel.Application.Modules.Hostel.Services
{
    public class HostelService : IHostelService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        private readonly IMediaUpload _mediaUpload;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IGenericRepository<Room, string> _genericRepository;
        private readonly INotificationService _notificationService;
        public HostelService(UserManager<ApplicationUser> userManager, INotificationService notificationService,
            IMediaUpload mediaUpload, ApplicationDbContext applicationDbContext, UnitOfWork unitOfWork, IGenericRepository<Room, string> genericRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _applicationDbContext= applicationDbContext;
            _mediaUpload = mediaUpload;
            _genericRepository = genericRepository;
            _userAccessor = userAccessor;
            _notificationService = notificationService;
        }


        public async Task<string> CreateHostel(HostelDTO hostelDTO)
        {
            var user = await _unitOfWork.LandlordRepository.FindandInclude(x => x.Id == hostelDTO.LandlordId, true);
            if (user is null) return null;

            var rooms = new List<Room>();

            foreach (var roomDto in hostelDTO.Rooms)
            {
                bool roomCreated = await CreateRoomForHostel(hostelDTO.LandlordId, roomDto);

                if (!roomCreated)
                {
                    return null;
                }
            }

            var hostelFrontViewPictures = await _mediaUpload.UploadPhotos(hostelDTO.HostelFrontViewPicture);

            string uploadHostelVideoUrl = null;

            if (hostelDTO?.VideoUrl != null)
            {
                var uploadHostelVideo = await _mediaUpload?.UploadVideo(hostelDTO.VideoUrl);
                uploadHostelVideoUrl = uploadHostelVideo?.Url;
            }

            var hostel = Domain.Hostel.Entities.Hostel.CreateHostelFactory(
                hostelDTO.LandlordId,
                hostelDTO.HostelName,
                hostelDTO.HostelDescription,
                hostelDTO.TotalRoom,
                hostelDTO.HomeSize,
                hostelDTO.Street,
                hostelDTO.Junction,
                hostelDTO.HostelCategory.GetEnumDescription(),
                hostelDTO.State,
                hostelDTO.PriceBudgetRange,
                hostelDTO.Country,
                hostelDTO.RulesAndRegulation,
                hostelDTO.HostelFacilities,
                hostelDTO.IsAnyRoomVacant,
                rooms,
                uploadHostelVideoUrl
                
                );

           // hostel.HostelFrontViewPicture?.AddRange(hostelFrontViewPictures.Select(result => result.Url);
            hostel.HostelFrontViewPicture?.AddRange(hostelFrontViewPictures.Select(x => 
                HostelPictures.CreateHostelOrRoomPicturesFactory(x.Url, x.PublicId, hostel.Id)));

            await _unitOfWork.HostelRepository.Add(hostel);
            await _unitOfWork.SaveAsync();


            return hostel.Id;
        }        
        public async Task<UpdateHostelResponse> UpdateHostel(string hostelId, HostelDTO hostelDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(hostelDTO.LandlordId);
            if (user is null) return null;

            var existinghostel = await _unitOfWork.HostelRepository.Find(h => h.Id == hostelId);
            if (existinghostel is null) return null;

            existinghostel.HostelName = hostelDTO.HostelName;
            existinghostel.HostelDescription = hostelDTO.HostelDescription;
            existinghostel.TotalRoom = hostelDTO.TotalRoom;
            existinghostel.HomeSize = hostelDTO.HomeSize;
            existinghostel.Street = hostelDTO.Street;
            existinghostel.Junction = hostelDTO.Junction;
            existinghostel.HostelCategory = hostelDTO.HostelCategory.GetEnumDescription();
            existinghostel.State = hostelDTO.State;
            existinghostel.Country = hostelDTO.Country;
            existinghostel.RulesAndRegulation = hostelDTO.RulesAndRegulation;
            existinghostel.HostelFacilities = hostelDTO.HostelFacilities; 
            existinghostel.IsAnyRoomVacant = hostelDTO.IsAnyRoomVacant;
           // existinghostel.Rooms = rooms;
            existinghostel.LastModifiedDate = DateTime.UtcNow;
            await _unitOfWork.HostelRepository.UpdateAsync(existinghostel);
            await _unitOfWork.SaveAsync();

            return new UpdateHostelResponse()
            {
                LandlordId = existinghostel.LandlordId,
                HostelId = existinghostel.Id,
                PriceBudgetRange= existinghostel.PriceBudgetRange,
                HostelName = existinghostel.HostelName,
                HomeSize= existinghostel.HomeSize,
                Country = existinghostel.Country,
                HostelCategory = existinghostel.HostelCategory,
                HostelDescription = existinghostel.HostelDescription,
                HostelFacilities= existinghostel.HostelFacilities,
                HostelFrontViewPicture = _mapper.Map<List<HostelPicturesDTO>>(existinghostel.HostelFrontViewPicture),
                State= existinghostel.State,    
                IsAnyRoomVacant = existinghostel.IsAnyRoomVacant,
                Junction = existinghostel.Junction,
                Rooms = _mapper.Map<List<RoomDTO>>(existinghostel.Rooms),
                RulesAndRegulation= existinghostel.RulesAndRegulation,
                Street = existinghostel.Street,
                TotalRoom = existinghostel.TotalRoom,
                VideoUrl = existinghostel.VideoUrl
            };
        }
           
        public async Task<ResultResponse<PagedList<HostelsResponse>>> GetAllHostels(HostelTypesParam hostelTypesParam)
        {

           var hostelsQuery = _applicationDbContext.Hostels
                .Include(r => r.Rooms)
                .Include(p => p.HostelFrontViewPicture)
                .OrderBy(d => d.CreatedDate)
                .Select( h => new HostelsResponse {
                    UserId = h.LandlordId,
                    HostelId = h.Id,
                    HostelCategory = h.HostelCategory,
                    Country = h.Country,
                    HomeSize = h.HomeSize,
                    HostelDescription = h.HostelDescription,
                    HostelFacilities = h.HostelFacilities,//.Select(o => new HostelFacilitiesDTO { FacilityName = o.FacilityName}).ToList(),
                    PriceBudgetRange = h.PriceBudgetRange,
                    NumberOfRoomsLeft = h.Rooms.Count(x => !x.IsRented && x.HostelId == h.Id),
                    Junction = h.Junction,
                   // HostelLikesCount = CountHostelLikes(h.Id),
                    RulesAndRegulation = h.RulesAndRegulation,//.Select(o => new HostelRulesAndRegulationsDTO { RuleAndRegulation = o.RuleAndRegulation }).ToList(),
                    State = h.State,
                    IsAnyRoomVacant = h.IsAnyRoomVacant,
                    Street = h.Street,
                    TotalRoom = h.TotalRoom,
                    HostelName = h.HostelName,
                    HostelFrontViewPicture =  _mapper.Map<List<HostelPicturesDTO>>(h.HostelFrontViewPicture),
                    
                })
                .Where(x => x.IsAnyRoomVacant == true)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(hostelTypesParam.SearchTerm))
            {
                hostelsQuery = hostelsQuery.SearchHostel(hostelTypesParam.SearchTerm);
            }
            else if (hostelTypesParam.HostelCategory != null)
            {
                hostelsQuery = hostelsQuery.Where(o => o.HostelCategory == hostelTypesParam.HostelCategory.GetEnumDescription());
            }
            else if (hostelTypesParam.PriceBudgetRange != null)
            {
                hostelsQuery = hostelsQuery.Where(r => r.PriceBudgetRange == hostelTypesParam.PriceBudgetRange);
            }

            if (hostelsQuery is null)
            {   
                return ResultResponse<PagedList<HostelsResponse>>.Failure(ResponseMessages.NotFound);
            }

            return ResultResponse<PagedList<HostelsResponse>>.Success(await PagedList<HostelsResponse>.CreateAsync(hostelsQuery, hostelTypesParam.PageNumber, hostelTypesParam.PageSize));
        }


        public async Task<List<GetMyHostelsDTO>> GetMyHostels(string landlordId)
        {
            var hostels = await _applicationDbContext.Hostels
                    .Include(x => x.Rooms)
                    .Include(p => p.HostelFrontViewPicture)
                    .Include(x => x.HostelLikes)
                    .Where(x => x.LandlordId == landlordId)
                    .AsNoTracking()
                    .ToListAsync();

            var hostelsDTO = new List<GetMyHostelsDTO>();

            foreach (var hostel in hostels)
            {
                var response = new GetMyHostelsDTO
                {
                    HostelId = hostel.Id,
                    HostelDescription = hostel.HostelDescription,
                    State = hostel.State,
                    Country = hostel.Country,
                    HomeSize = hostel.HomeSize,
                    HostelCategory = hostel.HostelCategory,
                    HostelFacilities = hostel.HostelFacilities,
                    HostelName = hostel.HostelName,
                    HostelLikesAndCount = await GetHostelLikedUsersAndCount(hostel.Id),
                    PriceBudgetRange = hostel.PriceBudgetRange,
                    //HostelLikesCount = hostel.HostelLikes.Count(x => x.LikedHostelId == hostel.Id),
                    NumberOfRoomsLeft = hostel.Rooms.Count(x => !x.IsRented && x.HostelId == hostel.Id),
                    Junction = hostel.Junction,
                    RulesAndRegulation = hostel.RulesAndRegulation,
                    Street = hostel.Street,
                    TotalRoom = hostel.TotalRoom,
                    HostelFrontViewPicture = _mapper.Map<List<HostelPicturesDTO>>(hostel.HostelFrontViewPicture),
                    Rooms = _mapper.Map<List<RoomToReturn>>(hostel.Rooms),
                };

                hostelsDTO.Add(response);
            }

            return hostelsDTO;
        }

        public async Task<HostelDetailsResponse> GetHostelById(string hostelId)
        {
            var hostel = await _applicationDbContext.Hostels
                .Include(x => x.Rooms)
                //.Include(x => x.Comments)
                //.Include(p => p.HostelFrontViewPicture)
                .Include(x => x.Landlord)
                    .ThenInclude(x => x.LandlordAgents)
                        .ThenInclude(x => x.Agent)
                            .ThenInclude(X => X.User)
                .Include(x => x.Landlord.User)
                .FirstOrDefaultAsync(x => x.Id == hostelId);

            if (hostel is not null)
            {
                var hostelDetailsToReturn = new HostelDetailsResponse();

                hostelDetailsToReturn.HostelDetails = new HostelDetails()
                {
                    HostelDescription = hostel.HostelDescription,
                    State = hostel.State,
                    Country = hostel.Country,
                    HomeSize = hostel.HomeSize,
                    HostelCategory = hostel.HostelCategory,
                    HostelFacilities = hostel.HostelFacilities,
                    HostelLikesAndCount = await GetHostelLikedUsersAndCount(hostelId),
                    HostelName = hostel.HostelName,
                    PriceBudgetRange = hostel.PriceBudgetRange,
                    //HostelLikesCount = await CountHostelLikes(hostelId),
                    NumberOfRoomsLeft = hostel.Rooms.Count(x => !x.IsRented && x.HostelId == hostel.Id),
                    Junction = hostel.Junction,
                    RulesAndRegulation = hostel.RulesAndRegulation,
                    Street = hostel.Street,
                    TotalRoom = hostel.TotalRoom,
                    HostelFrontViewPicture = _mapper.Map<List<HostelPicturesDTO>>(hostel.HostelFrontViewPicture),
                };
                hostelDetailsToReturn.Rooms = _mapper.Map<List<RoomToReturn>>(hostel.Rooms);
               // hostelDetailsToReturn.CommentDTO = _mapper.Map<List<CommentDTO>>(hostel.Comments);
                hostelDetailsToReturn.LandlordProfile = _mapper.Map<LandlordProfileToDisplay>(hostel.Landlord);
                if (hostel.Landlord.LandlordAgents != null && hostel.Landlord.LandlordAgents.Any())
                {
                    hostelDetailsToReturn.LandlordProfile = _mapper.Map<LandlordProfileToDisplay>(hostel.Landlord.LandlordAgents.ToList()[0].Landlord);
                }
               // hostelDetailsToReturn.LandlordProfile = _mapper.Map<LandlordProfileToDisplay>(hostel?.Landlord?.LandlordAgents?.ToList()[0]?.Landlord);
                return hostelDetailsToReturn;
            }
            
            return null;          
        }
          
        public async Task<bool> CreateRoomForHostel(string userId, RoomDTO roomDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(userId);
            if (user is null) return false;

            var hostel =  await _unitOfWork.HostelRepository.FindandInclude(x => x.Id == roomDTO.HostelId, true); 
            if (hostel is null) return false;

            var room = Room.CreateRoomForHostelFactory(roomDTO.RoomNumber, roomDTO.Price, roomDTO.Duration,
                                                          roomDTO.RoomFacilities, roomDTO.IsRented, roomDTO.HostelId);
            var photoUploadResults = await _mediaUpload.UploadPhotos(roomDTO.Files);

            room.RoomPictures?.AddRange(photoUploadResults.Select(x =>
                HostelPictures.CreateHostelOrRoomPicturesFactory(x.Url, x.PublicId, room.HostelId)));
           
            await _unitOfWork.RoomRepository.Add(room);
            await _unitOfWork.SaveAsync();
            
            return true;
        }   

       /* public async Task<List<HostelsResponse>> GetMyLikedHostels(string userId)
        {
            var likedHostels = await _applicationDbContext.HostelLikes
                                
                              // .Include(x => x.LikedHostel)
                               //.ThenInclude(x => x.Rooms)                              
                               .Where(x => x.SourceUserId == userId)
                               .Select(x => x.LikedHostel)
                               .ToListAsync(); 

            var likedHostelsResponse = likedHostels.Select(h => new HostelsResponse
            {
                UserId = h.LandlordId,
                HostelId = h.Id,
                HostelCategory = h.HostelCategory,
                Country = h.Country,
                HomeSize = h.HomeSize,
                HostelDescription = h.HostelDescription,
                HostelFacilities = h.HostelFacilities,
                PriceBudgetRange = h.PriceBudgetRange,
               // NumberOfRoomsLeft = h.Rooms.Count(x => !x.IsRented && x.HostelId == h.Id),
                Junction = h.Junction,
                //HostelLikesCount = h.HostelLikes.Count(x => x.LikedHostelId == h.Id),
                RulesAndRegulation = h.RulesAndRegulation,
                State = h.State,
                IsAnyRoomVacant = h.IsAnyRoomVacant,
                Street = h.Street,
                TotalRoom = h.TotalRoom,
                HostelName = h.HostelName,
                HostelFrontViewPicture = h.HostelFrontViewPicture
            }).ToList();

            return likedHostelsResponse;

        }*/

        public async Task<List<string>> GetMyLikedHostels(string userId)
        {
            var likedHostels = await _applicationDbContext.HostelLikes                           
                               .Where(x => x.SourceUserId == userId)
                               .Select(x => x.LikedHostel.Id)
                               .ToListAsync();

            return likedHostels;
        }

        /*public async Task<List<LikedUserDTO>> GetHostelLikedUsers(string hostelId)
        {
            var likedUsers = await _applicationDbContext.HostelLikes
                             .Where(x => x.LikedHostelId == hostelId)
                             .Select(x => x.SourceUser)
                             .ToListAsync();

            var likedUsersResponse = likedUsers.Select(u => new LikedUserDTO
            {
                Name = $"{u.FirstName} {u.LastName}",
                UserId = u.Id,
                ProfilePicture = u.ProfilePhotoURL
            }).ToList();

            return likedUsersResponse;
        }*/

        public async Task<HostelLikesAndCount> GetHostelLikedUsersAndCount(string hostelId)
        {
            var likedUsers = await _applicationDbContext.HostelLikes
                             .Where(x => x.LikedHostelId == hostelId)
                             .Select(x => x.SourceUser.Id)
                             .ToListAsync();

            return  new HostelLikesAndCount
            {
                HostelLikedUsers = likedUsers,
                HostelLikesCount = likedUsers.Count()
            };
        }

        private int CountHostelLikes(string hostelId)
        {
            var likedUsers =  _applicationDbContext.HostelLikes
                             .Where(x => x.LikedHostelId == hostelId)
                             .Select(x => x.SourceUser.Id)
                             .ToList();

            return likedUsers.Count();
        }

        public async Task<(IEnumerable<RoomCollectionsDTO> roomDTOs, string ids)> CreateRoomCollectionAsync(string landlordId, string hostelId, IEnumerable<RoomToCreate> roomsToCreates)
        {
            var user = await _userAccessor.CheckIfTheUserExist(landlordId);
            if (user is null) throw new RoomDetailsExceptionResponse("User not found", System.Net.HttpStatusCode.NotFound);

            var hostel = await _unitOfWork.HostelRepository.FindandInclude(x => x.Id == hostelId, true);
            if (hostel is null) throw new RoomDetailsExceptionResponse("Hostel not found", System.Net.HttpStatusCode.NotFound);

            var roomData = new List<Room>();

            foreach (var roomToCreate in roomsToCreates)
            {
                var room = Room.CreateRoomForHostelFactory(
                    roomToCreate.RoomNumber,
                    roomToCreate.Price,
                    roomToCreate.Duration,
                    roomToCreate.RoomFacilities,
                    roomToCreate.IsRented,
                    hostelId
                );

                roomData.Add(room);
                await _unitOfWork.RoomRepository.Add(room);
            }

            await _unitOfWork.SaveAsync();

            var response = _mapper.Map<IEnumerable<RoomCollectionsDTO>>(roomData);

            var ids = string.Join(",", response.Select(c => c.RoomId));

            return (rooms: response, roomIds: ids);
        }


        public async Task<RoomUpdateResponse> UpdateARoomForHostel(string userId, RoomDTO roomDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(userId);
            if (user is null) return null;

            var hostel = await GetHostelById(roomDTO.HostelId);
            if (hostel is null) return null;

            var existingRoom = await _unitOfWork.RoomRepository.Find(h => h.Id == roomDTO.HostelId);
            if (existingRoom is null) return null;

            existingRoom.RoomNumber = roomDTO.RoomNumber;
            existingRoom.Price = roomDTO.Price;
            existingRoom.Duration = roomDTO.Duration;
            existingRoom.RoomFacilities = roomDTO.RoomFacilities; 
            existingRoom.LastModifiedDate = DateTime.UtcNow;
            await _unitOfWork.RoomRepository.UpdateAsync(existingRoom);
            await _unitOfWork.SaveAsync();

            return new RoomUpdateResponse()
            {
                HostelId = existingRoom.HostelId,
                RoomNumber= existingRoom.RoomNumber,
                IsRented= roomDTO.IsRented,
                Duration = roomDTO.Duration,
                Files= roomDTO.Files,
                Price = roomDTO.Price,
                RoomFacilities = roomDTO.RoomFacilities
            };
        }
        
        public async Task<RoomToReturn> GetARoomForHostel(string hostelId, string roomId)
        {
            var checkIfHostelExist = await GetHostelById(hostelId);
            if (checkIfHostelExist is null)
            {
                return null;
            }
            var room = await _unitOfWork.RoomRepository.FindByCondition(h => h.HostelId.Equals(hostelId) && h.Id.Equals(roomId), false).SingleOrDefaultAsync();

            if (room is null)
            {
                return null;
            }

            var roomDto = _mapper.Map<RoomToReturn>(room);
            return roomDto;
        }

        public async Task<List<RoomToReturn>> GetAllRoomsForHostel(string hostelId)
        {
            var hostelRooms = await _unitOfWork.RoomRepository.FindByCondition(x => x.HostelId == hostelId, true).ToListAsync();

            if (hostelRooms is null)
            {
                return null;
            }

            var roomsDto = _mapper.Map<List<RoomToReturn>>(hostelRooms);
            return roomsDto;
        }


        public async Task<bool> DeleteProductPicture(string userId, string pictureId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;

            var getPicture = await _unitOfWork.HostelPictureRepository.GetById(pictureId);
            if (getPicture is null) return false;

            var cloudinaryDelete = await _mediaUpload.DeletePhoto(getPicture.PublicId);
            if (cloudinaryDelete is null)
                return false;

            await _unitOfWork.HostelPictureRepository.Remove(getPicture);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> AddHostelLike(string sourceId, string hostelLikeId)
        {
            var sourceUser =  await _userAccessor.CheckIfTheUserExist(sourceId);
            if (sourceUser is null) return false;

            var hostelLiked = await _unitOfWork.HostelRepository.FindandInclude(x => x.Id == hostelLikeId, true);
            if (hostelLiked is null && hostelLiked.Count() < 0) return false;

            var hostelLike = await _unitOfWork.HostelLikesRepository.Find(x => x.SourceUserId == sourceId && x.LikedHostelId == hostelLikeId);
            if (hostelLike is null)
            {
                var likeHostel = HostelLikes.CreateHostelLikesFactory(sourceId, hostelLikeId);
                await _unitOfWork.HostelLikesRepository.Add(likeHostel);
                var save = await _unitOfWork.SaveAsync();
                if (save > 0)
                {

                    await _notificationService.CreateNotificationAsync(new NotificationDTO
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = sourceId,
                        NotificationType = NotificationType.HostelLike,
                        Content = String.Format(NotificationMessageConstants.ReferralRegistration, sourceUser.UserName),
                        UserProfilePicUrl = sourceUser.ProfilePhotoURL,
                        IsRead = false,
                        NotificationTypeValueId = hostelLikeId
                    });
                }
            }
            else
            {
                await _unitOfWork.HostelLikesRepository.Remove(hostelLike);
                await _unitOfWork.SaveAsync();
            }

            return true;
        }

        public async Task<Comment> CreateHostelCommentAsync(Comment createComment)
        {
            var comment = await _unitOfWork.CommentRepository.Add(createComment);
            await _unitOfWork.SaveAsync();
            return comment;
        }

        public async Task<int> DeleteHostelCommentAsync(string commentId, string hostelId)
        {
            var getComment = await _unitOfWork.CommentRepository.Find(x => x.Id == commentId && x.HostelId == hostelId);
            var deleteComment = await _unitOfWork.CommentRepository.Remove(getComment);
            return deleteComment;
        }

        public async Task<Comment> UpdateHostelCommentAsync(Comment comment)
        {
            await _unitOfWork.CommentRepository.UpdateAsync(comment);
            await _unitOfWork.SaveAsync();
            return comment;
        }

        public async Task<CommentResponse> GetAllHostelCommentsAsync(string hostelId)
        {
            var comments = await _applicationDbContext.Comments
                           .OrderByDescending(x => x.LastModifiedDate)
                           .Where(x => x.HostelId == hostelId)
                           .AsNoTracking()
                           .ToListAsync();

            if (comments is null)
                return null;

            CommentResponse commentResponse = new();
            Parallel.ForEach(comments, async x =>
            {
                var comment = await GetHostelCommentByCommentIdAsync(x.Id);
                if (comment is not null)
                {
                    var getParentComments = await GetHierarchicalComments(comment.ParentCommentId);
                    commentResponse.Comments = getParentComments;
                }
            });

                return commentResponse;
         }

        public async Task<List<Comment>> GetHierarchicalComments(string parentCommentId = "")
        {
            var comments = await GetAllHostelParentCommentsAsync(parentCommentId);

            foreach (var comment in comments)
            {
                comment.Comments = await GetHierarchicalComments(comment.Id);
            }
            return comments.ToList();
        }

        public async Task<IEnumerable<Comment>> GetAllHostelParentCommentsAsync(string parentCommentId)
        {
            var parentComments = await _applicationDbContext.Comments
                            .Where(x => x.ParentCommentId == parentCommentId)
                            .AsNoTracking()
                            .ToListAsync();
            return parentComments;
        }

        public async Task<Comment> GetHostelCommentByCommentIdAsync(string commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(commentId);
            return comment;
        }


       /* public async Task<ResultResponse<CommentDTO>> CreateComment(CreateCommentDTO createCommentDTO)
        {
           //var hostel = await _unitOfWork.HostelRepository.FindandInclude(x => x.Id == createCommentDTO.HostelId, true);
           var hostel = await _applicationDbContext.Hostels.FindAsync(createCommentDTO.HostelId);
            if (hostel is null) return null;

            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.LastName == _userAccessor.GetUsername());
            if (user is null) return null;

            var comment = new Comment
            {
                Author = user,
                Hostel = hostel,
                UserComment = createCommentDTO.UserComment,
            };

            hostel.Comments.Add(comment);
            var success = await _applicationDbContext.SaveChangesAsync() > 0;

            if (success)
            {
                return ResultResponse<CommentDTO>.Success(_mapper.Map<CommentDTO>(comment));
            }

            return ResultResponse<CommentDTO>.Failure(ResponseMessages.FailedCreation);

        }

        public async Task<ResultResponse<List<CommentDTO>>> GetComments(string hostelId)
        {
            var comments = await _applicationDbContext.Comments
                .Where(x => x.Hostel.Id == hostelId)
                .OrderByDescending(x => x.CreatedDate)
                .ProjectToType<CommentDTO>()
                .ToListAsync();

            return ResultResponse<List<CommentDTO>>.Success(comments);
        }
       

        private async Task<T> CheckForNull<T>(T entity)
        {
            if (entity is null)
                return entity;

            return entity;
        }*/

    }
}
