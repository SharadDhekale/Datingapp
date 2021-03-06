import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  users: any;
  title = 'Datting App';
  constructor(private httpclient: HttpClient) {

  }
  ngOnInit() {
    this.getUsers();
  }
  getUsers() {
    this.httpclient.get("https://localhost:5001/api/Users").subscribe(response => {
      this.users = response
    }, error => {
      console.log(error);
    });
  }
}
