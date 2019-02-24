using System.Data.Entity.ModelConfiguration;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.EF
{
    public class DemoDbModelConfig : EntityTypeConfiguration<DemoDbModel>
    {
        public DemoDbModelConfig()
        {
            ToTable("DemoTable");
            HasKey(m => m.Id);
            Property(m => m.Name).IsRequired()
                                 .IsUnicode()
                                 .IsVariableLength()
                                 .HasMaxLength(35);
            Property(m => m.Created).IsRequired();
            Property(m => m.Modified).IsOptional();
            Property(m => m.DemoCommonInfoModelId).IsRequired();
            HasRequired(m => m.DemoCommonInfoModel)
                .WithMany(i => i.DemoModels)
                .WillCascadeOnDelete(false);
        }
    }
}