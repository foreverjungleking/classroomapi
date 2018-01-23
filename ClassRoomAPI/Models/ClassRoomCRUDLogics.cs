namespace ClassRoomAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class ClassRoomCRUDLogics : DbContext
    {
        // Your context has been configured to use a 'ClassRoomCRUDLogics' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ClassRoomAPI.Models.ClassRoomCRUDLogics' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ClassRoomCRUDLogics' 
        // connection string in the application configuration file.
        public ClassRoomCRUDLogics()
            : base("name=ClassRoomCRUDLogics")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
    }

    public class Student
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        [Required]
        [RegularExpression("^[0-9]{5}$")]
        public string id { get; set; }
        [Key]
        [Required]
        [Column(Order = 1)]
        [Range(0, 99)]
        public int classNumber { get; set; }
        [Required]
        [Range(0, 100)]
        public int score { get; set; }
    }

    public class Class
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Range(0, 99)]
        public int classNumber { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(1)]
        public string teacher { get; set; }
    }
}