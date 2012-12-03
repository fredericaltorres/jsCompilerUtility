/*
    jsonB - TypeScript Data Types
    (c) Torres Frederic 2012
*/

module jsonB {

    export var Version = 1.0;

    export module Const {

        export var READONLY = 0 << 1; 
        export var EDITABLE = 0 << 1; 
        export var DATE     = 0 << 1; 
    }
    export interface MetadataProperty { 

        Attr        : number;
        Values?     : string[]; // LIST MONOSELECT, MULTISELECT
        Template?   : string;   // EMAIL
        Caption?    : string;   // Any property to change the name display
        Range?      : number[]; // EDITABLE Number
        Format?     : string;   // DATE
    }
    export interface NewProperty { 

        Name        : string;
        Value       : any;
        AutoReset?  : bool;
    }
    export interface Template { 

        Name        : string;
        Value       : string[];
    }
    export interface Annotations {
     
        Title?		: string;
        Subtitle?   : string;
        Latitude	: number; 
        Longitude	: number;
    }
    export interface GeoLocation { 

        Title?		: string;
        Latitude	: number; 
        Longitude	: number;
        Annotations?: Annotations[];
    }
    interface _ActionGetPost { 

        // Method:Get, Post
        Url?        : string;
    }
    interface _ActionEMail { 
 
        // Method:EMail
        To?         : string;
        Subject?    : string;
        Template?   : string;
    }
    export interface Action extends _ActionGetPost, _ActionEMail { 

        Name        : string;
        Method      : string;   // Get, Post, Email
        AutoPop?    : bool;     // Default:False
    }
    export interface Metadata {

        _Caption            : string;
        _IDProperty		    : string;
        _DisplayOrder       : string;
        _Sort?			   	: string;
        _Actions?           : Action[];
        _Templates?         : Template[];
        _NewProperties?     : NewProperty[];
        _Properties?        : MetadataProperty[];
    }
}
