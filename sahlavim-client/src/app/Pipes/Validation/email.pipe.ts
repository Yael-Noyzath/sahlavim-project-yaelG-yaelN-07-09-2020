import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'email'
})
export class EmailPipe implements PipeTransform {

  transform(value:string) {
    var isEmail=/^[a-z0-9]+@$‏/; 

    // var isEmail=/^[a-z0-9][a-z0-9-_\.]+@([a-z]|[a-z0-9]?[a-z0-9-]+[a-z0-9])\.[a-z0-9]{2,10}(?:\.[a-z]{2,10})?$‏/; 
if(isEmail.test(value)==false)
{
  return 'הכנס כתובת מייל תקינה';
}

}

}
