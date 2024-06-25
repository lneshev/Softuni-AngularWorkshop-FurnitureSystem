using System;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities
{
    public interface ITrackableEntityBase
    {
        DateTimeOffset CreatedAt { get; set; }
        Guid CreatedById { get; set; }
        DateTimeOffset? ModifiedAt { get; set; }
        Guid? ModifiedById { get; set; }
    }
}