using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HotelAppWPF.Models
{
    public enum TypeSizeEnumModel : byte
    {
        Undefined = 0,
        SGL,
        DBL,
        DBL_TWN,
        TRPL,
        DBL_EXB,
        TRPL_EXB
    }
    public enum TypeComfortEnumModel : byte
    {
        Undefined = 0,
        Standart,
        Suite,
        De_Luxe,
        Duplex,
        Family_Room,
        Honeymoon_Room
    }
}
