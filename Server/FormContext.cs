using Microsoft.EntityFrameworkCore;
using SingleGridFormWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleGridFormWebApp.Server
{
    public class FormContext : DbContext
    {
        /// <summary>
        /// Magic string.
        /// </summary>
        public static readonly string RowVersion = nameof(RowVersion);

        /// <summary>
        /// Magic strings.
        /// </summary>
        public static readonly string FormDb = nameof(FormDb).ToLower();

        /// <summary>
        /// List of <see cref="Form"/>.
        /// </summary>
        public DbSet<Form> Forms { get; set; }

        /// <summary>
        /// List of <see cref="Column"/>.
        /// </summary>
        public DbSet<Column> Columns { get; set; }

        /// <summary>
        /// List of <see cref="Row"/>.
        /// </summary>
        public DbSet<Row> Rows { get; set; }

        /// <summary>
        /// List of <see cref="RowData"/>.
        /// </summary>
        public DbSet<RowData> RowDataSet { get; set; }

        public FormContext(DbContextOptions<FormContext> options)
            : base(options)
        {
            Debug.WriteLine($"{ContextId} context created.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>().ToTable("Form");
            modelBuilder.Entity<Column>().ToTable("Column");
            modelBuilder.Entity<Row>().ToTable("Row");
            modelBuilder.Entity<RowData>().ToTable("RowData");

            modelBuilder.Entity<RowData>().HasOne(p => p.Column);
            modelBuilder.Entity<RowData>().HasOne(p => p.Form);
        }
    }
}
