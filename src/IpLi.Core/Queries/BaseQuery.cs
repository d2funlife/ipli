using System;
using System.Collections.Generic;
using IpLi.Core.Exceptions;
using IpLi.Core.Helpers;

namespace IpLi.Core.Queries
{
    public abstract class BaseQuery
    {
        protected const Int32 MaxLimit = 1000;
        protected const Int32 DefaultLimit = 20;
        protected const Int32 DefaultOffset = 0;
        protected const String OrderDesc = "desc";
        protected const Char ParamDelimiter = ',';
        protected const Char SubParameterDelimiter = ':';

        private Int32 _offset;
        private Int32 _limit;
        private String _orderValue;
        
        private List<(String orderField, Boolean isAsc)> _orderParameters;

        protected (String orderField, Boolean isAsc) DefaultOrder;

        protected BaseQuery(Int32 offset = DefaultOffset,
                         Int32 limit = DefaultLimit)
        {
            Limit = limit;
            Offset = offset;
        }

        protected BaseQuery(Int32 offset,
                         Int32 limit,
                         String order)
            : this(offset, limit)
        {
            _orderValue = order;
        }

        protected BaseQuery(String order)
            : this()
        {
            _orderValue = order;
        }

        public Int32 Offset
        {
            get => _offset;
            set =>
                _offset = value < 0
                    ? DefaultOffset
                    : value;
        }

        public Int32 Limit
        {
            get => _limit;
            set
            {
                if(value <= 0)
                {
                    _limit = DefaultLimit;
                }
                else
                {
                    _limit = (value > MaxLimit)
                        ? MaxLimit
                        : value;
                }
            }
        }

        public Boolean IgnoreOrder { get; set; }

        public List<(String orderField, Boolean isAsc)> OrderParameters
        {
            get
            {
                if(_orderParameters == null)
                {
                    _orderParameters = GetOrderParameters();
                }

                return _orderParameters;
            }
        }

        private List<(String orderField, Boolean isAsc)> GetOrderParameters()
        {
            if(String.IsNullOrEmpty(_orderValue))
            {
                return GetDefaultOrder();
            }

            var parts = _orderValue.Split(new[] {ParamDelimiter}, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<(String orderField, Boolean isAsc)>(parts.Length);
            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];

                var subParts = part.Split(new[] {SubParameterDelimiter}, StringSplitOptions.RemoveEmptyEntries);
                if(subParts.Length > 2)
                {
                    continue;
                }

                if(subParts.Length == 2)
                {
                    var isAsc = !subParts[1]
                       .Equals(OrderDesc, StringComparison.OrdinalIgnoreCase);
                    result.Add((subParts[0]
                                   .ToLowerInvariant(), isAsc));
                }

                if(subParts.Length == 1)
                {
                    result.Add((subParts[0]
                                   .ToLowerInvariant(), true));
                }
            }

            return result.IsEmpty()
                ? GetDefaultOrder()
                : result;
        }

        private List<(String orderField, Boolean isAsc)> GetDefaultOrder()
        {
            if(IgnoreOrder)
            {
                return null;
            }
            
            if(DefaultOrder.orderField == null)
            {
                throw new QueryIsNotCorrectException("orderBy", "Default order is not specified");
            }

            return new List<(String orderField, Boolean isAsc)> {DefaultOrder};
        }

        protected void SetLimit(Int32 limit)
        {
            _limit = limit;
        }
    }
}