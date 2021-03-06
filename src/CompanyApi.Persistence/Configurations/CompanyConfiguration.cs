﻿using CompanyApi.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyApi.Persistence.Configurations
{
	public class CompanyConfiguration
	{
		public CompanyConfiguration(EntityTypeBuilder<Company> entity)
		{
			// Table
			entity.ToTable("Companies");

			// Properties
			entity.HasKey(e => e.CompanyId);
			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(255);

			// Relationships
			entity.HasMany(a => a.Employees)
				.WithOne(b => b.Company)
				.HasForeignKey(c => c.CompanyId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
