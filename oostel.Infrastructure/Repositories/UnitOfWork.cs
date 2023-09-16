using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserProfiles.Entities;
using Oostel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private GenericRepository<UserProfile, string> userProfileRepository;
        private GenericRepository<Hostel, string> hostelRepository;

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

        public GenericRepository<UserProfile, string> UserProfileRepository
        {
            get
            {
                if (userProfileRepository == null)
                {
                    userProfileRepository = new GenericRepository<UserProfile, string>(_context);
                }
                return userProfileRepository;
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
