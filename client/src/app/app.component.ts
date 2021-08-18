import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  users: any;
  title = 'Datting App';
  constructor(private httpclient: HttpClient, private accountService: AccountService) {

  }
  ngOnInit() {
    this.getUsers();
    this.setcurrentUser();
  }

  setcurrentUser() {
    //const user:User = JSON.parse(localStorage.getItem('user')|| '{}');
    const user:User = JSON.parse(localStorage.getItem('user')!);
    this.accountService.setCurrentUser(user);
  }
  getUsers() {
    this.httpclient.get("https://localhost:5001/api/Users").subscribe(response => {
      this.users = response
    }, error => {
      console.log(error);
    });
  }
}
