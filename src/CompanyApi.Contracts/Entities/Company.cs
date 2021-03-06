﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyApi.Contracts.Entities.Base;
using Newtonsoft.Json;

namespace CompanyApi.Contracts.Entities
{
	[Serializable]
	[JsonObject(IsReference = false)]
	public class Company : BaseAuditEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CompanyId { get; set; }

		[Required]
		public string Name { get; set; }

        // Navigation property
		public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public override string ToString() => $"{CompanyId}, {Name}";
	}
}