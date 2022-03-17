using System;
using System.Collections.Generic;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    /// <summary>
    /// User-friendly enum value names system
    /// </summary>
    public class Enums
    {
        [AttributeUsage(AttributeTargets.Field)]
        public class Label : Attribute
        {
            public string label;

            public Label(string label)
            {
                this.label = label;
            }
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class Order : Attribute
        {
            public int order;

            public Order(int order)
            {
                this.order = order;
            }
        }

        /// <summary>
        /// Returns an array of the enum values, sorted by their [Order] attribute.
        /// This allows the custom enums to be in any order, retaining the original values that correspond to the built-in Unity enum.
        /// </summary>
        static public OrderedEnum[] GetOrderedEnumValues(Type enumType)
        {
            if(!enumType.IsEnum)
            {
                Debug.LogError("Not an enum type: " + enumType);
                return null;
            }

            List<OrderedEnum> orderedEnums = new List<OrderedEnum>();
            var fields = enumType.GetFields();
            foreach (var field in fields)
            {
                var orders = (Order[])field.GetCustomAttributes(typeof(Order), false);
                var labels = (Label[])field.GetCustomAttributes(typeof(Label), false);
                if (orders != null && orders.Length > 0)
                {
                    Enum value = (Enum)field.GetValue(null);
                    string name = value.ToString();
                    if(labels != null && labels.Length > 0)
                    {
                        name = labels[0].label;
                    }

                    orderedEnums.Add(new OrderedEnum()
                    {
                        value = value,
                        order = orders[0].order,
                        displayName = name
                    });
                }
            }
            orderedEnums.Sort((x,y) => x.order.CompareTo(y.order));
            return orderedEnums.ToArray();
        }

        public struct OrderedEnum
        {
            public Enum value;
            public string displayName;
            public int order;
        }
    }
}