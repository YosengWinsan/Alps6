export enum AlpsActionResultCode
  {
    Ok = 1,
    Error = -1,
    Warning = 0
  }

  export class AlpsActionResponse{
      resultCode:AlpsActionResultCode;
      message:string[];
      data:any;
  } 