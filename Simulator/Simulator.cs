using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


//namespace Simulator;

public static class Simulator
{
    private static BlApi.IBl? Bl = BlApi.Factory.Get();
    private static Thread? _thread;
    private static volatile bool beContinue = true;
    private static event EventHandler<Tuple<BO.Order,int>>? updateSimulator;
    private static event EventHandler? stopSimulator;
    public static void RegistrToStopEvent(EventHandler handler)
    {
        stopSimulator += handler;
    }

    public static void UnRegistrToStopEvent(EventHandler handler)
    {
        if(stopSimulator!.GetInvocationList().Contains(handler))
        {
            stopSimulator -= handler;
        }
    }

    public static void RegistrToUpdateEvent(EventHandler<Tuple<BO.Order, int>> handler)
    {
        updateSimulator += handler;
    }

    public static void UnRegistrToUpdateEvent(EventHandler<Tuple<BO.Order, int>> handler)
    {
        if (updateSimulator!.GetInvocationList().Contains(handler))
        {
            updateSimulator -= handler;
        }
    }

    public static void StartSimulator()
    {
        _thread = new Thread(() =>
        {
            while (beContinue && Bl!.Order.OldestOrder != null)
            {
                try
                {
                    var Order = Bl!.Order.GetOrder(Bl?.Order.OldestOrder() ?? throw new NullReferenceException());

                    var ProcessingTime = new Random().Next(3,10);
                    var EstimaredTime = new Random().Next(ProcessingTime - 2, ProcessingTime + 2);

                    updateSimulator?.Invoke(null, new Tuple<BO.Order, int>(Order, EstimaredTime));
                    try
                    {
                        Thread.Sleep(ProcessingTime * 1000);
                    }
                    catch (ThreadInterruptedException) {}
 
                    if (!beContinue) { break; }
                    if (Order.Status == BO.Enums.OrderStatus.Order_Confirmed)
                    {
                        Bl!.Order.ShippingUpdate(Order.ID);
                    }
                    else if (Order.Status == BO.Enums.OrderStatus.Order_Sent)
                    {
                        Bl!.Order.SupplyUpdateOrder(Order.ID);
                    }
                    else stopSimulator?.Invoke(null, EventArgs.Empty);
                }
                catch (BO.NotExistException ex) { Console.WriteLine(ex); StopSimulator(); }
            }
            stopSimulator?.Invoke(null, EventArgs.Empty);
        });
        _thread.Start();    
    }
    public static void StopSimulator()
    {
        beContinue = false;
        _thread?.Interrupt();    
    }
}
