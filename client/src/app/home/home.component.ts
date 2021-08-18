import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode: boolean = false;
  constructor() { }

  ngOnInit(): void {
  }

  ToggleRegisterMode() {
    this.registerMode = !this.registerMode;
  }
  CancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
