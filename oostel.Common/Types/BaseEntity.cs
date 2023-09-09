using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Types
{
    public abstract class BaseEntity<T> : IEquatable<BaseEntity<T>>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region Constructors and properties
        protected BaseEntity(T id)
        {
            if (!IsValidId(id))
                throw new ArgumentException();

            Id = id;
        }

        protected BaseEntity()
        {
            if (Id is not null && !IsValidId(Id))
                throw new ArgumentException();
        }
        #endregion



        #region IEquatable implementation
        public bool Equals(BaseEntity<T> other)
        {
            return Id.GetHashCode() == other.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity<T>);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region Private methods
        private bool IsValidId(T id)
        {
            return id is int || id is Guid || id is string;
        }
        #endregion

    }
}
