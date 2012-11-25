/*
    jsonB - TypeScript Data Structure Model
    (c) Torres Frederic 2012
*/
var Const = {
    DATE : 1 << 0,
    TIME : 1 << 1,
    EDITABLE : 1 << 2,
    READONLY : 1 << 3,
}

interface MetadataProperty { 

    Attr        : number;
    Values?     : string[]; // LIST MONOSELECT, MULTISELECT
    Template?   : string;   // EMAIL
    Caption?    : string;   // Any property to change the name display
    Range?      : number[]; // EDITABLE Number
    Format?     : string;   // DATE
}
interface Template { 

    Name        : string;
    Value       : string[];
}
interface Annotations {
     
    Title?		: string;
    Subtitle?   : string;
    Latitude	: number; 
    Longitude	: number;
}
interface GeoLocation { 

    Title?		: string;
    Latitude	: number; 
    Longitude	: number;
    Annotations?: Annotations[];
}
interface ActionGetPost { 

    // Method:Get, Post
    Url?        : string;
}
interface ActionEMail { 
 
    // Method:EMail
    To?         : string;
    Subject?    : string;
    Template?   : string;
}
interface Action extends ActionGetPost, ActionEMail { 

    Name        : string;
    Method      : string;   // Get, Post, Email
    AutoPop?    : bool;     // Default:False
}
interface Metadata {

    _Caption            : string;
    _IDProperty		    : string;
    _DisplayOrder       : string;
    _Sort?			   	: string;
    _Actions?           : Action[];
    _Templates?         : Template[];
    _NewProperties?     : MetadataProperty[];
}
 