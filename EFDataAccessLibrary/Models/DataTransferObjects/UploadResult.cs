﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class UploadResult
{
    public string? FileName { get; set; }
    public string? StoredFileName { get; set; }
}
