using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFree.Simple.Application.Contracts.Common;

namespace XFree.Simple.Application.Common.Impl
{
    /// <summary>
    /// 
    /// </summary>
    internal class CodeGenerator : ICodeGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        public CodeGenerator()
        {
            // TODO实现多实例分配
            WorkerId = 1;
            DatacenterId = 1;
            _sequence = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public async Task<string> Create(string prefix)
        {
            return await Task.FromResult($"{prefix}{NextId()}");
        }

        #region snowflake

        //借用https://github.com/stulzq/snowflake-net

        //基准时间
        public const long Twepoch = 1288834974657L;
        //机器标识位数
        const int WorkerIdBits = 5;
        //数据标志位数
        const int DatacenterIdBits = 5;
        //序列号识位数
        const int SequenceBits = 12;
        //序列号ID最大值
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);
        //机器ID偏左移12位
        private const int WorkerIdShift = SequenceBits;
        //数据ID偏左移17位
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        //时间毫秒左移22位
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        public long WorkerId { get; protected set; }

        public long DatacenterId { get; protected set; }

        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }

        readonly object _lock = new();

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception(string.Format("时间戳必须大于上一次生成ID的时间戳.  拒绝为{0}毫秒生成id", _lastTimestamp - timestamp));
                }

                //如果上次生成时间和当前时间相同,在同一毫秒内
                if (_lastTimestamp == timestamp)
                {
                    //sequence自增，和sequenceMask相与一下，去掉高位
                    _sequence = (_sequence + 1) & SequenceMask;
                    //判断是否溢出,也就是每毫秒内超过1024，当为1024时，与sequenceMask相与，sequence就等于0
                    if (_sequence == 0)
                    {
                        //等待到下一毫秒
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    //如果和上次生成时间不同,重置sequence，就是下一毫秒开始，sequence计数重新从0开始累加,
                    //为了保证尾数随机性更大一些,最后一位可以设置一个随机数
                    _sequence = 0;//new Random().Next(10);
                }

                _lastTimestamp = timestamp;
                return ((timestamp - Twepoch) << TimestampLeftShift) | (DatacenterId << DatacenterIdShift) | (WorkerId << WorkerIdShift) | _sequence;
            }
        }

        // 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        private static readonly DateTime Jan1st1970 = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // 获取当前的时间戳
        protected virtual long TimeGen()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        #endregion
    }
}
