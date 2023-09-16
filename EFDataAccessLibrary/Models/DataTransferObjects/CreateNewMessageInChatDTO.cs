using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class CreateNewMessageInChatDTO
{
    public string sender { get; set; }
    public string receiver { get; set; }
    public string messageBody { get; set; }
}
