using System;
using Microsoft.SPOT;
using schemas.example.org.SimpleService;

namespace WsdlServerSample
{
    internal sealed class SimpleServiceImplementation : ISimpleService
    {
        #region ISimpleService Members
        void ISimpleService.OneWay(OneWay req)
        {
            Debug.Print("OneWay Param=" + req.Param);
        }

        TwoWayResponse ISimpleService.TwoWay(TwoWayRequest req)
        {
            Debug.Print("OneWay X=" + req.X + ", Y=" + req.Y);
            // return sum of x and y
            TwoWayResponse resp = new TwoWayResponse();
            resp.Sum = req.X + req.Y;
            return resp;
        }

        TypeCheckResponse ISimpleService.TypeCheck(TypeCheckRequest req)
        {
            throw new NotImplementedException();
        }

        AnyCheckResponse ISimpleService.AnyCheck(AnyCheckRequest req)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
