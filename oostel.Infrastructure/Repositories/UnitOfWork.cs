using MapsterMapper;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserMessage;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Domain.UserWallet;
using Oostel.Infrastructure.Data;

namespace Oostel.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {

        ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        private GenericRepository<UserOTP, string> userOTPRepository;
        private GenericRepository<Landlord, string> landlordRepository;
        private GenericRepository<Student, string> studentRepository;
        private GenericRepository<Hostel, string> hostelRepository;
        private GenericRepository<Room, string> roomRepository;
        private GenericRepository<OpenToRoommate, string> openToRoommateRepository;
        private GenericRepository<HostelLikes, string> hostelLikesRepository;
        private GenericRepository<StudentLikes, string> studentLikesRepository;
        private GenericRepository<Comment, string> commentRepository;
        private GenericRepository<Wallet, string> walletRepository;
        private GenericRepository<Message, string> messageRepository;

        public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper);

        public GenericRepository<UserOTP, string> UserOTPRepository
        {
            get
            {

                if (userOTPRepository == null)
                {
                    userOTPRepository = new GenericRepository<UserOTP, string>(_context);
                }
                return userOTPRepository;
            }
        }

        public GenericRepository<Wallet, string> WalletRepository
        {
            get
            {

                if (walletRepository == null)
                {
                    walletRepository = new GenericRepository<Wallet, string>(_context);
                }
                return walletRepository;
            }
        }


        public GenericRepository<Room, string> RoomRepository
        {
            get
            {

                if (roomRepository == null)
                {
                    roomRepository = new GenericRepository<Room, string>(_context);
                }
                return roomRepository;
            }
        }

        public GenericRepository<Comment, string> CommentRepository
        {
            get
            {

                if (commentRepository == null)
                {
                    commentRepository = new GenericRepository<Comment, string>(_context);
                }
                return commentRepository;
            }
        }

        public GenericRepository<OpenToRoommate, string> OpenToRoommateRepository
        {
            get
            {
                if (openToRoommateRepository == null)
                {
                    openToRoommateRepository = new GenericRepository<OpenToRoommate, string>(_context);
                }
                return openToRoommateRepository;
            }
        }

        public GenericRepository<HostelLikes, string> HostelLikesRepository
        {
            get
            {
                if (hostelLikesRepository == null)
                {
                    hostelLikesRepository = new GenericRepository<HostelLikes, string>(_context);
                }
                return hostelLikesRepository;
            }
        }

        public GenericRepository<StudentLikes, string> StudentLikesRepository
        {
            get
            {
                if (studentLikesRepository == null)
                {
                    studentLikesRepository = new GenericRepository<StudentLikes, string>(_context);
                }
                return studentLikesRepository;
            }
        }

        public GenericRepository<Hostel, string> HostelRepository
        {
            get
            {
                if(hostelRepository == null)
                {
                    hostelRepository = new GenericRepository<Hostel, string>(_context);
                }
                return hostelRepository;
            }
        }

        public GenericRepository<Landlord, string> LandlordRepository
        {
            get
            {
                if (landlordRepository == null)
                {
                    landlordRepository = new GenericRepository<Landlord, string>(_context);
                }
                return landlordRepository;
            }
        }

        public GenericRepository<Student, string> StudentRepository
        {
            get
            {
                if (studentRepository == null)
                {
                    studentRepository = new GenericRepository<Student, string>(_context);
                }
                return studentRepository;
            }
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {

            _context.ChangeTracker.DetectChanges();

            var changes = _context.ChangeTracker.HasChanges();

            return changes;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
