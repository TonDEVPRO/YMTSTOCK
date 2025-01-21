
using System;

namespace RfqEbr.Data.Contracts.Repository
{
    public interface ICustomRepository
    {
        string GetDbInfo();
        DateTime GetSysDate();
	    string GetMaxRfqNo();
    }
}
