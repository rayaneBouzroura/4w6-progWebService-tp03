import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AddTokenInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(request.url != 'https://localhost:7008/api/Utilisateurs/Register' && request.url != 'https://localhost:7008/api/Utilisateurs/Login'){
      request = request.clone({
        setHeaders :{
          'Content-Type' : 'application/json',
          'Authorization' : 'Bearer ' + localStorage.getItem('token')
        }

      })
      console.log(localStorage.getItem('token'));
    }

    return next.handle(request);
  }
}
