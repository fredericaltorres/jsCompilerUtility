/// <reference path="Internal\jsonB.d.ts" />
/// JsonB Metadata
var metadata: jsonB.Metadata;

metadata = {
    
	"_Caption"       	: "{{name}}",
	"_IDProperty"		: "name",
	"_DisplayOrder"     : "name,Quantity,Unit,@Order,QuantityRequested,OrderID,OrderDate,DeviceID,@Product Information,Description",
    "_Sort"			   	: "name",
    "_NewProperties"	: [
		    					{ 
		    						Name:		"QuantityRequested",
		    						Value:		0,
		    						AutoReset:	true,
		    					}, 
		    					{
		    						Name:		"DeviceName",
		    						Value:		"$DeviceName",
		    					},
		    					{
		    						Name:		"DeviceID",
		    						Value:		"$DeviceID",
		    					},
		    					{
		    						Name:		"OrderDate",
		    						Value:		"$Now",
		    						AutoReset:	true,
		    					},
		    					{
		    						Name:		"OrderID",
		    						Value:		"$Guid",
		    						AutoReset:	true,
		    					},
            			],
	"_Actions"          : [
		    					{		     
		    						"Name":		"Order",
		    						"Method":	"Get",
		    						"Url":  	"http://www.jyos.net/jsonB/ws/GenericWS.asp?name={{name}}&QuantityRequested={{QuantityRequested}}&DeviceName={{DeviceName}}&DeviceID={{DeviceID}}&OrderID={{OrderID}}&OrderDate={{OrderDate}}",
		    						"AutoPop": 	true,
		    					},
		    					{
		    						"Name":	 "Email Order",
		    						Method:  "EMail",
		    						Template:"EMail",
		    						To:		 "FredericALTorres@gmail.com",
		    						Subject: "Order {{name}}, Device {{DeviceName}}",
		    						AutoPop: false,
		    					},
		    			],
	"_Templates"        : [
								{
                                    Name:"EMail",
                                    Value: [
                                        	
			                            "{{name}}",
			                            "-------------------------------------------",
			                            "OrderID			{{OrderID}}				",
			                            "OrderDate			{{OrderDate}}			",
			                            "Quantity Requested {{QuantityRequested}}	",
			                            "",
			                            "DeviceName 		{{DeviceName}}			",
			                            "DeviceID			{{DeviceID}}			",
			                            "",
			                            "Unit     			{{Unit}}				",
			                            "Time	  			{{$Now}}				",
			                            "-------------------------------------------",
			                            ]
		                        },
		                    ],
	"_Properties"		: [ 
								{ Name:"Name", 				Attr:jsonB.Const.READONLY, Caption:"Name"},
								{ Name:"Quantity", 			Attr:jsonB.Const.READONLY }, 
								{ Name:"QuantityRequested", Attr:jsonB.Const.EDITABLE, Range:[0, 100] },
								{ Name:"OrderDate", 		Attr:jsonB.Const.DATE, Format:"MM/DD/YYYY HH:mm:SS" }, 
						]
}