using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller;

internal static class Queries
{
    #region Stored procedures for table Wine
    public const string Wine_Get =      "spWine_Get";
    public const string Wine_GetAll =   "spWine_GetAll";
    public const string Wine_Insert =   "spWine_Insert";
    public const string Wine_Delete =   "spWine_Delete";
    public const string Wine_Update =   "spWine_Update";
    #endregion


}
