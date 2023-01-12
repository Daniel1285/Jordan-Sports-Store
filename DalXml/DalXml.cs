﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml :IDal
{
    private DalXml() { }
    public static IDal Instance { get; } = new DalXml();
    public IOrder Order { get; } = new DalOrder();

    public IProduct Product { get; } = new DalProduct();

    public IOrderItem OrderItem { get; } = new DalOrderItem();



}
