using System;

namespace SGRH._Domain.Base
{
    public abstract class BaseEntity : AuditEntity
    {
        public int Id { get; set; }

        protected BaseEntity() { }
    }
}