using System;
using System.Linq;
using System.Collections.Generic;

namespace Exercise2
{
    /// <summary>
    /// This is the simplest VWAP strategy implementation.
    /// The following enhancement could be done in the future:
    /// 1. Add Aggressiveness parameter
    /// 2. Max percentage volumn for each chile order
    /// 3. Support limit order for parent order
    /// 4. Support pro-rate the volumn calculation when the order sliced between two time spread
    /// </summary>
    internal class VWAPStrategy
    {
        private ParentOrder parentOrder;
        private SortedList<TimeSpan, Tick> priceHistory;

        private IEnumerable<KeyValuePair<TimeSpan, Tick>> range;

        public VWAPStrategy(ParentOrder parentOrder, SortedList<TimeSpan, Tick> priceHistory)
        {
            this.parentOrder = parentOrder;
            this.priceHistory = priceHistory;

            range = from price in priceHistory
                    where price.Key >= parentOrder.StartTime && price.Key <= parentOrder.EndTime
                    select price;
        }

        internal ChildOrder Slice(TimeSpan timeSpan)
        {
            if (timeSpan <= parentOrder.StartTime)
                return null;

            int totalVolumnWithOrderTimeRange = (from total in range select total.Value.Volumn).Sum();

            int quantityToBeExecuted = (from price in range where price.Key <= timeSpan select price.Value.Volumn).Sum();

            int unslicedQuantity = parentOrder.Quantity - parentOrder.SlicedQuantity;

            ChildOrder childOrder = new ChildOrder();
            childOrder.OrderType = OrderType.Market;
            childOrder.Quantity = Math.Min(unslicedQuantity, (int) Math.Ceiling((double) parentOrder.Quantity * quantityToBeExecuted / totalVolumnWithOrderTimeRange) - parentOrder.SlicedQuantity);

            parentOrder.SlicedQuantity += childOrder.Quantity;

            return childOrder;
        }
    }
}