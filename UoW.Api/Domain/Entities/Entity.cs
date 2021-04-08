using System;

namespace UoW.Api.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool Removed { get; private set; }

        public void Remove()
        {
            Removed = true;
        }

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}