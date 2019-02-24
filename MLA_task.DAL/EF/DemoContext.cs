﻿using System.Data.Entity;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.EF
{
    public class DemoContext : DbContext
    {
        public DemoContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public IDbSet<DemoDbModel> DemoDbModels { get; set; }
        public IDbSet<DemoCommonInfoDbModel> DemoCommonInfoModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DemoDbModelConfig())
                                       .Add(new DemoCommonInfoDbModelConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}