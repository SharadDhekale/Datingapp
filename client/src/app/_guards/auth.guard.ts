import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService:AccountService, private toastrService:ToastrService){}

  canActivate(): Observable<boolean> {
      
    return this.accountService.currentUser$.pipe(
      map<User,boolean>(user=>{
        if(user) return true;
        this.toastrService.error('UnAuthorized');
        return false;
      })
        
    );
 
  }
  
}
