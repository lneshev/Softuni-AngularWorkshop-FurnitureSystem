using MoravianStar.Dao;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities
{
    public class TrackableEntityBase<TId> : EntityBase<TId>, ITrackableEntityBase
    {
        [ForeignKey(nameof(CreatedById))]
        public virtual UserEntity CreatedBy { get; set; }
        public Guid CreatedById { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [ForeignKey(nameof(ModifiedById))]
        public virtual UserEntity ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public override bool IsNew()
        {
            return base.IsNew() || CreatedAt == default || CreatedById == default;
        }
    }
}