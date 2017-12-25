using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{

    public class MonitorAnalysis
    {
        /// <summary>
        /// 判断间隔 秒级
        /// </summary>
        int JudgeSpanSeconds { get; set; }

        /// <summary>
        /// 间隔频次水平 层级数与 LimitLevel 对应
        /// </summary>
        int[] LevelPvCount { get; set; }

        /// <summary>
        /// 临界值（小于阈值不处理）
        /// </summary>
        int Threshold { get; set; }

        /// <summary>
        /// 获取访问记录
        /// </summary>
        Func<uint, List<DateTime>> GetRecords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Attempt(uint ip)
        {
            var records = GetRecords(ip);
            var sum = records.Count;
            if (sum < Threshold) return ;

            var group = RecordsGroup(records);
            var weight = AccumulateWeight(sum, group);
            var limit = WeightMapping(weight);
        }


        /// <summary>
        /// 按时间间隔分组
        /// </summary>
        /// <param name="records">访问集合</param>
        Dictionary<double, int> RecordsGroup(List<DateTime> records)
        {
            var min = records.Min();
            var max = records.Max();
            // var group = new Dictionary<double,List<DateTime>>();
            var group = new Dictionary<double, int>();
            var baseTime = DateTime.Now;
            foreach (var item in records)
            {
                var span = item - baseTime;
                var key = Math.Ceiling(span.TotalSeconds / JudgeSpanSeconds);
                if (group.ContainsKey(key))
                {
                    var value = group[key];
                    value++;
                    group[key] = value;
                }
                else
                {
                    group.Add(key, 1);
                }
            }

            return group;
        }


        /// <summary>
        /// 访问限制累计
        /// </summary>
        /// <param name="total"> 访问总量</param>
        /// <param name="group"></param>
        /// <returns></returns>
        int AccumulateWeight(int total, Dictionary<double, int> group)
        {
            int weight = 0;
            var average = total / JudgeSpanSeconds;

            //均值判断 （访问并发量）
            if (average > 20)
            {
                weight++;
            }

            //方差判断 （访问分布，规律明显则疑似爬虫）
            //if()

            var list = group.Select(s => s.Value);

            return weight;
        }

        /// <summary>
        /// 访问权重限制级别对照
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        LimitResult WeightMapping(int weight)
        {
            if (weight > 10)
            {

            }
            else if (weight > 5)
            {

            }
            else if (weight > 1)
            {

            }
            return new LimitResult();
        }

        /// <summary>
        /// 求随机数平均值方法
        /// </summary>
        static double Ave(double[] a)
        {
            double sum = 0;
            foreach (double d in a)
            {

                sum = sum + d;
            }
            double ave = sum / a.Length;

            return ave;
        }

        /// <summary>
        /// 求数方差
        /// </summary>
        static double Var(double[] v)
        {
            double sum1 = 0;
            for (int i = 0; i < v.Length; i++)
            {
                double temp = v[i] * v[i];
                sum1 = sum1 + temp;
            }

            double sum = 0;
            foreach (double d in v)
            {
                sum = sum + d;
            }

            double var = sum1 / v.Length - (sum / v.Length) * (sum / v.Length);
            return var;
        }

        /// <summary>
        /// 求正态分布的随机数
        /// </summary>
        static void Fenbu(double[] f)
        {
            //double fenbu=new double[f.Length ];
            for (int i = 0; i < f.Length; i++)
            {
                double a = 0, b = 0;
                a = Math.Sqrt((-2) * Math.Log(f[i], Math.E));
                b = Math.Cos(2 * Math.PI * f[i]);
                f[i] = a * b * 0.3 + 1;
            }
        }

    }

    /// <summary>
    /// 访问频次校验
    /// </summary>
    public class AccessFrequency : IAccessStrategy
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public bool Attempt(VisitorContext context)
        {
            if (context.Judgement == null) return false;
            if (context.Judgement.GradeInMonitoring == null) return false;
            var grade = context.Judgement.GradeInMonitoring();
            if (grade > 0) return context.ChangeResult(true, LimitLevel.Limit);
            return false;
        }
    }
}
