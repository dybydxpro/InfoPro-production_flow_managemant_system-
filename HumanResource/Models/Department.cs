﻿using System.ComponentModel.DataAnnotations;
using HumanResource.Models.Entity;

namespace HumanResource.Models
{
    public class Department: IEntity
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string DepartmentName { get; set; }

        #region Audit Fields
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        #endregion
    }
}