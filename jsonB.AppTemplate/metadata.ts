/// <reference path="Internal\jsonB.d.ts" />
/// JsonB Metadata
var metadata: jsonB.Metadata;

metadata = {
    
    "_Caption"       	: "{{name}}",
    "_IDProperty"		: "name",
    "_DisplayOrder"     : "name,Quantity,Unit,@Order,QuantityRequested,OrderID,OrderDate,DeviceID,@Product Information,Description,WebSite",
    "_Sort"			   	: "name",
    "_NewProperties"	: [
                                { 
                                    "Name"			: "QuantityRequested",
                                    "Value"			: 0,
                                    "AutoReset"		: true,
                                }, 
                                {
                                    "Name"			: "DeviceName",
                                    "Value"			: "$DeviceName",
                                },
                                {
                                    "Name"			: "DeviceID",
                                    "Value"			: "$DeviceID",
                                },
                                {
                                    "Name"			: "OrderDate",
                                    "Value"			: "$Now",
                                    "AutoReset"		: true,
                                },
                                {
                                    "Name"			: "OrderID",
                                    "Value"			: "$Guid",
                                    "AutoReset"		: true,
                                },
                                {
                                    "Name"			: "WebSite",
                                    "Value"			: "http://www.freebase.com/view/en/{{name}}",
                                }
    ],
    "_Actions"          : [    	
                                /*
                                {		     
                                    "Name"			: "Order Get",
                                    "Method"		: "Get",
                                    "Url"			: "http://www.jyos.net/jsonB/ws/GenericWS.asp?name={{name}}&QuantityRequested={{QuantityRequested}}&DeviceName={{DeviceName}}&DeviceID={{DeviceID}}&OrderID={{OrderID}}&OrderDate={{OrderDate}}",
                                    "AutoPop"		: true,
                                    "Success"		: "{{QuantityRequested}} {{name}} ordered", 
                                    "Failure" 		: "Order failed"
                                }, */
                                {
                                    "Name"			: "Order",
                                    "Method"		: "Post",
                                    "Url"			: "http://www.jyos.net/jsonB/ws/GenericWSPost.asp",
                                    "Parameters"	: "name={{name}}&QuantityRequested={{QuantityRequested}}&DeviceName={{DeviceName}}&DeviceID={{DeviceID}}&OrderID={{OrderID}}&OrderDate={{OrderDate}}",
                                    "ContentType"	: "application/x-www-form-urlencoded", 
                                    "AutoPop"		: true,
                                    "Success"		: "{{QuantityRequested}} {{name}} ordered", 
                                    "Failure" 		: "Order failed"
                                },
                                {
                                    "Name"			: "Order By EMail",
                                    "Method"		: "EMail",
                                    "Template"		: "EMail",
                                    "To"			: "FredericALTorres@gmail.com",
                                    "Subject"		: "{{name}} Order, Device {{DeviceName}}",
                                    "AutoPop"		: false,
                                },
                                /*
                                {		     
                                    "Name"			: "Delete",
                                    "Method"		: "Get",
                                    "Url"			: "http://www.jyos.net/jsonB/ws/GenericWS.asp?name={{name}}",
                                    "AutoPop"		: true,
                                    "Success"		: "{{name}} deleted", 
                                    "Failure" 		: "Deletion product '{{name}}' failed"
                                }, */
    ],
    "_Templates"        : [
                                {
                                    "Name"  :"EMail",
                                    "Value" : [
                                            "OrderID: {{OrderID}}                           ",
                                            "OrderDate: {{OrderDate}}			            ",
                                            "QuantityRequested: {{QuantityRequested}}       ",
                                            "Unit: {{Unit}}                                 ",
                                            "                                               ",
                                            "DeviceName: {{DeviceName}}                     ",
                                            "DeviceID: {{DeviceID}}                         ",
                                            "                                               ",
                                            "Time: {{$Now}}                                 ",
                                    ]
                                },
    ],
    "_Properties"		: [
                                { "Name" : "Name", 				"Attr":"READONLY", 	"Caption":"Name"				     },
                                { "Name" : "Quantity", 			"Attr":"READONLY" 									     },
                                { "Name" : "QuantityRequested", "Attr":"EDITABLE", 	"Range":[0, 100] 				     },
                                { "Name" : "OrderDate", 		"Attr":"DATE", 		"Format":"MM/DD/YYYY HH:mm:SS" 	     },
                                { "Name" : "WebSite", 			"Attr":"HTML", 		"Caption":"WebSite about {{name}}"   },
    ]
}