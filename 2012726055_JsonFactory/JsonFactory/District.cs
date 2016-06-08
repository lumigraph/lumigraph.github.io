//
//  District.cs
//  JsonFactory
//
//  Created by Merong World on 06/04/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFactory
{
    class District
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public District(string name, int code)
        {
            this.Name = name;
            this.Code = code;
        }
    }
}
