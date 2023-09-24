using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Infrastructure.Data;

namespace Oostel.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {

        ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
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
