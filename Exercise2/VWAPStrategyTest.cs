using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    class VWAPStrategyTest
    {
        private SortedList<TimeSpan, Tick> priceHistory;

        [SetUp]
        public void SetUp()
        {
            priceHistory = new SortedList<TimeSpan, Tick>();
            priceHistory.Add(new TimeSpan(10, 0, 0), new Tick(10.0, 100000));
            priceHistory.Add(new TimeSpan(10, 10, 0), new Tick(11.0, 80000));
            priceHistory.Add(new TimeSpan(10, 20, 0), new Tick(12.0, 50000));
            priceHistory.Add(new TimeSpan(10, 30, 0), new Tick(10.0, 50000));
            priceHistory.Add(new TimeSpan(10, 40, 0), new Tick(9.8, 10000));
            priceHistory.Add(new TimeSpan(10, 50, 0), new Tick(10.5, 20000));
            priceHistory.Add(new TimeSpan(11, 0, 0), new Tick(11.0, 50000));
        }

        [Test]
        public void SliceChildOrderBaseOnPreviousPriceHistory()
        {
            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(11, 0, 0);
            int orderQuantity = 10000;

            ParentOrder parentOrder = new ParentOrder(Side.Buy, startTime, endTime, OrderType.Market, orderQuantity);

            VWAPStrategy vwap = new VWAPStrategy(parentOrder, priceHistory);

            ChildOrder childOrder = vwap.Slice(new TimeSpan(10, 0, 0));
            Assert.GreaterOrEqual(parentOrder.SlicedQuantity, 0 * orderQuantity);

            childOrder = vwap.Slice(new TimeSpan(10, 10, 0));
            Assert.GreaterOrEqual(parentOrder.SlicedQuantity, 0.5 * orderQuantity);

            childOrder = vwap.Slice(new TimeSpan(10, 20, 0));
            Assert.GreaterOrEqual(parentOrder.SlicedQuantity, 0.638888889 * orderQuantity);

            childOrder = vwap.Slice(new TimeSpan(10, 30, 0));
            Assert.GreaterOrEqual(parentOrder.SlicedQuantity, 0.777777778 * orderQuantity);

            childOrder = vwap.Slice(new TimeSpan(10, 40, 0));
            Assert.GreaterOrEqual(parentOrder.SlicedQuantity, 0.805555556 * orderQuantity);

            childOrder = vwap.Slice(new TimeSpan(10, 50, 0));
            Assert.GreaterOrEqual(parentOrder.SlicedQuantity, 0.861111111 * orderQuantity);

            childOrder = vwap.Slice(new TimeSpan(11, 0, 0));
            Assert.GreaterOrEqual(parentOrder.SlicedQuantity, 1 * orderQuantity);
        }
    }
}
