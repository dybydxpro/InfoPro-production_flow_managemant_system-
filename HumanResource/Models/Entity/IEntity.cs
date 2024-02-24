﻿using System.ComponentModel.DataAnnotations;

namespace HumanResource.Models.Entity;

public interface IEntity
{
    int Id { get; set; }
    int CompanyId { get; set; }

    bool IsDeleted { get; set; }
    [Timestamp]
    byte[] Timestamp { get; set; }
    int CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    int UpdatedBy { get; set; }
    DateTime UpdatedOn { get; set; }
}