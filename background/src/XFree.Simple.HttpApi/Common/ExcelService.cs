using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.Common
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public class ExcelService : IScopedDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ErrorMessageService _errorMessageService;

        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessageService"></param>
        /// <param name="stringLocalizerFactory"></param>
        public ExcelService(ErrorMessageService errorMessageService, 
            IStringLocalizerFactory stringLocalizerFactory)
        {
            _errorMessageService = errorMessageService;
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <summary>
        /// 获取excel内容
        /// </summary>
        /// <param name="filePath">excel文件路径</param>
        /// <returns></returns>
        public static DataTable ImportExcel(string filePath)
        {
            DataTable dt = new();
            using (FileStream fsRead = File.OpenRead(filePath))
            {
                IWorkbook wk = null;
                //获取后缀名
                string extension = filePath[filePath.LastIndexOf(".")..].ToString().ToLower();
                //判断是否是excel文件
                if (extension == ".xlsx" || extension == ".xls")
                {
                    //判断excel的版本
                    if (extension == ".xlsx")
                    {
                        wk = new XSSFWorkbook(fsRead);
                    }
                    else
                    {
                        wk = new HSSFWorkbook(fsRead);
                    }

                    //获取第一个sheet
                    ISheet sheet = wk.GetSheetAt(0);
                    //获取第一行
                    IRow headrow = sheet.GetRow(0);
                    //创建列
                    for (int i = headrow.FirstCellNum; i < headrow.Cells.Count; i++)
                    {
                        //  DataColumn datacolum = new DataColumn(headrow.GetCell(i).StringCellValue);
                        var datacolum = new DataColumn("F" + (i + 1));
                        dt.Columns.Add(datacolum);
                    }
                    //读取每行,从第二行起
                    for (int r = 1; r <= sheet.LastRowNum; r++)
                    {
                        bool result = false;
                        DataRow dr = dt.NewRow();
                        //获取当前行
                        IRow row = sheet.GetRow(r);
                        //读取每列
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            ICell cell = row.GetCell(j); //一个单元格
                            dr[j] = GetCellValue(cell); //获取单元格的值
                                                        //全为空则不取
                            if (dr[j].ToString() != "")
                            {
                                result = true;
                            }
                        }
                        if (result == true)
                        {
                            dt.Rows.Add(dr); //把每行追加到DataTable
                        }
                    }
                }

            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpPostedFile"></param>
        /// <param name="columnItems"></param>
        /// <returns></returns>
        public List<T> ImportExcel<T>(IFormFile httpPostedFile, List<ColumnItem<T>> columnItems) where T : class, new()
        {
            ExcelDataType excelDataType = ExcelDataType.Import;
            var resultList = new List<T>();
            IWorkbook wk = null;
            //获取后缀名
            string extension = httpPostedFile.FileName[httpPostedFile.FileName.LastIndexOf(".")..].ToString().ToLower();
            //判断是否是excel文件
            if (extension == ".xlsx" || extension == ".xls")
            {
                using (var stream = httpPostedFile.OpenReadStream())
                {
                    //判断excel的版本
                    if (extension == ".xlsx")
                    {
                        wk = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        wk = new HSSFWorkbook(stream);
                    }
                }
                var needExportColumnItems = columnItems.Where(w => w.IsNeed(excelDataType)).ToList();
                //获取第一个sheet
                ISheet sheet = wk.GetSheetAt(0);
                //获取第一行
                IRow headrow = sheet.GetRow(0);
                var missingColumnNames = needExportColumnItems.Where(w => !headrow.Cells.Any(a => a?.ToString() == w.ColumnDisplayName.Localize(_stringLocalizerFactory))).ToList();
                //缺少列
                if (missingColumnNames.Any())
                {
                    _errorMessageService.ThrowMessageParam(FriendlyExceptionCode.ExcelImport001, string.Join(",", missingColumnNames.Select(s => s.ColumnDisplayName.Localize(_stringLocalizerFactory))));
                }
                var columnItemDict = needExportColumnItems.GroupBy(g => g.ColumnDisplayName.Localize(_stringLocalizerFactory).ToString()).ToDictionary(d => d.Key, d => d);
                //读取每行,从第二行起
                for (int r = 1; r <= sheet.LastRowNum; r++)
                {
                    T result = new();
                    //获取当前行
                    IRow row = sheet.GetRow(r);
                    //读取每列
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        ICell cell = row.GetCell(j);
                        var columnItem = columnItemDict[headrow.Cells[j].ToString()].First();
                        try
                        {
                            columnItem.ImportSet(result, GetCellValue(cell));
                        }
                        catch
                        {
                            _errorMessageService.ThrowMessageParam(FriendlyExceptionCode.ExcelImport003, $"{j}", $"{cell}");
                        }
                    }
                    resultList.Add(result);
                }
            }
            if (resultList.Count == 0)
            {
                _errorMessageService.ThrowMessage(FriendlyExceptionCode.ExcelImport002);
            }

            return resultList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="columnItems"></param>
        /// <param name="excelDataType"></param>
        /// <returns></returns>
        public byte[] Export<T>(IReadOnlyList<T> list, List<ColumnItem<T>> columnItems,ExcelDataType excelDataType = ExcelDataType.Export)
        {
            var hssfWorkbook = new HSSFWorkbook();
            ICellStyle headStyle = hssfWorkbook.CreateCellStyle();
            headStyle.BorderBottom = BorderStyle.Thin;
            headStyle.BorderLeft = BorderStyle.Thin;
            headStyle.BorderRight = BorderStyle.Thin;
            headStyle.BorderTop = BorderStyle.Thin;
            headStyle.Alignment = HorizontalAlignment.Center;
            IFont font = hssfWorkbook.CreateFont();
            font.IsBold = true;
            headStyle.SetFont(font);
            //创建Excel工作表
            ISheet sheet1 = hssfWorkbook.CreateSheet("sheet");
            IRow row1 = sheet1.CreateRow(0);
            var cellIndex = 0;
            var needExportColumnItems = columnItems.Where(w => w.IsNeed(excelDataType)).ToList();
            // 创建表头
            needExportColumnItems.ForEach(f =>
            {
                sheet1.SetColumnWidth(cellIndex, 30 * 256);
                row1.CreateCell(cellIndex).SetCellValue(f.ColumnDisplayName.Localize(_stringLocalizerFactory));
                row1.Cells[cellIndex].CellStyle = headStyle;
                cellIndex++;
            });
            var rowIndex = 1;
            ICellStyle style = hssfWorkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            var resultEntityType = typeof(T);
            // 数据列
            foreach (var objInfo in list)
            {
                cellIndex = 0;
                var row = sheet1.CreateRow(rowIndex);
                needExportColumnItems.ForEach(f =>
                {
                    row.CreateCell(cellIndex).SetCellValue(f.ExportGet(objInfo));
                    row.Cells[cellIndex].CellStyle = style;
                    cellIndex++;
                });
                rowIndex++;
            }
            using var memoryStream = new MemoryStream();
            hssfWorkbook.Write(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToIList<T>(DataTable dt) where T : class, new()
        {
            var ts = new List<T>();
            string tempName;
            foreach (DataRow dr in dt.Rows)
            {
                var t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 对单元格进行判断取值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank: //空数据类型 这里类型注意一下，不同版本NPOI大小写可能不一样,有的版本是Blank（首字母大写)
                    return string.Empty;
                case CellType.Boolean: //bool类型
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric: //数字类型
                    if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else //其它数字
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.Unknown: //无法识别类型
                default: //默认类型
                    return cell.ToString();//
                case CellType.String: //string 类型
                    return cell.StringCellValue;
                case CellType.Formula: //带公式类型
                    try
                    {
                        var e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objInfo"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private static string GetValue<T>(T objInfo, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
            {
                return (propertyInfo.GetValue(objInfo) as DateTime?)?.ToString(Const.DateTimeConfig.DEFAULT_FORMAT);
            }
            if (propertyInfo.PropertyType.IsEnum)
            {
                var enumValue = propertyInfo.GetValue(objInfo);
                var enumDesc = (enumValue as Enum).GetDescription();
                return enumDesc?.Description ?? enumValue?.ToString();
            }
            return propertyInfo.GetValue(objInfo)?.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ColumnItem<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public ILocalizableString ColumnDisplayName { get; set; }

        /// <summary>
        /// 导出
        /// </summary>
        public Func<T, string> ExportGet { get; set; }

        /// <summary>
        /// 导入赋值
        /// </summary>
        public Action<T, string> ImportSet { get; set; }

        /// <summary>
        /// 导出时忽略
        /// </summary>
        public bool IgnoreExport { get; set; }

        /// <summary>
        /// 导入时忽略
        /// </summary>
        public bool IgnoreImport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelDataType"></param>
        /// <returns></returns>
        public bool IsNeed(ExcelDataType excelDataType) 
        {
            return excelDataType switch
            {
                ExcelDataType.Export => !IgnoreExport,
                ExcelDataType.Import => !IgnoreImport,
                ExcelDataType.Template => !IgnoreImport,
                _ => !IgnoreImport,
            };
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum ExcelDataType
    {
        /// <summary>
        /// 导出
        /// </summary>
        Export = 1,
        /// <summary>
        /// 导入
        /// </summary>
        Import = 2,
        /// <summary>
        /// 模板
        /// </summary>
        Template = 3
    }

}