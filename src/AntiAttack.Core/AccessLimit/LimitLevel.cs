using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{
    /// <summary>
    /// 访问限制级别
    /// </summary>
    public enum LimitLevel
    {
        /// <summary>
        /// 不限制
        /// </summary>
        None,

        /// <summary>
        /// 限制访问 分为多 Grade 
        /// </summary>
        Limit,

        /// <summary>
        /// 完全限制
        /// </summary>
        All,
    }

    /// <summary>
    /// 限制处理结果
    /// </summary>
    public class LimitResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LimitResult(LimitLevel level = LimitLevel.None, int grade = 0)
        {
            Level = level;
            Grade = grade;
        }

        /// <summary>
        /// 限制水平
        /// </summary>
        public LimitLevel Level { get; set; }

        /// <summary>
        /// 限制等级
        /// </summary>
        public int Grade { get; set; }

       
    }

   

}
