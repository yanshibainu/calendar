using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace calendar.Entities
{
    /// <summary>
    ///     假日設定
    /// </summary>
    [Table("GE_Workday")]
    public class Workday
    {
        /// <summary>
        ///     機關代碼
        /// </summary>

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     機關代碼
        /// </summary>
        [Required]
        [StringLength(30)]
        public string TenantId { get; set; }

        /// <summary>
        ///     日期
        /// </summary>
        public DateTime WorkDay { get; set; }

        /// <summary>
        ///     類型
        /// </summary>
        public WorkdayType Type { get; set; }

        /// <summary>
        ///     起始時間
        /// </summary>
        public DateTime? DayStart { get; set; }

        /// <summary>
        ///     結束時間
        /// </summary>
        public DateTime? DayEnd { get; set; }

        /// <summary>
        ///     天數
        /// </summary>
        public double DayHourCount { get; set; }

        /// <summary>
        ///     時戳
        /// </summary>
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }

    public enum WorkdayType : short
    {
        Allday = 1,
        Holiday = 2
    }
}