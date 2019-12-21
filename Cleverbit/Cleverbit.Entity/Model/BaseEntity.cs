using System;

namespace Cleverbit.Entity.Model
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
