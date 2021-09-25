import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  usermodel: any = {};

  constructor(public accountService: AccountService, private router: Router, private toastrService : ToastrService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.usermodel).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/members');
    })
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
