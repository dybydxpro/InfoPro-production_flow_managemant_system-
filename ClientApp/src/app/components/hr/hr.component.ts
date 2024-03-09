import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-hr',
  templateUrl: './hr.component.html',
  styleUrl: './hr.component.scss'
})
export class HrComponent implements OnInit {

  constructor(private router: Router, private location: Location) {}

  ngOnInit(): void {
    
  }

  goBack(): void {
    this.location.back();
  }
}
