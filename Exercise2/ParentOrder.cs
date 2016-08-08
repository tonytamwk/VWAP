using System;

namespace Exercise2
{
    internal class ParentOrder
    {
        private Side side;
        private TimeSpan endTime;
        private TimeSpan startTime;
        private OrderType orderType;
        private int quantity;
        public int SlicedQuantity { get; set; }

        public ParentOrder(Side side, TimeSpan startTime, TimeSpan endTime, OrderType orderType, int quantity)
        {
            this.side = side;
            this.startTime = startTime;
            this.endTime = endTime;
            this.orderType = orderType;
            this.quantity = quantity;
            SlicedQuantity = 0;
        }

        public TimeSpan StartTime { get { return startTime; } }

        public TimeSpan EndTime { get { return endTime; } }

        public int Quantity { get { return quantity; } }
    }
}