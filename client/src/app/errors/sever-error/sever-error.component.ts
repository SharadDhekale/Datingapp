import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sever-error',
  templateUrl: './sever-error.component.html',
  styleUrls: ['./sever-error.component.css']
})
export class SeverErrorComponent implements OnInit {
  error: any;
  constructor(private router: Router) {
    var currentstate = this.router.getCurrentNavigation();
    this.error = currentstate?.extras?.state?.error;
  }

  ngOnInit(): void {
  }

}
