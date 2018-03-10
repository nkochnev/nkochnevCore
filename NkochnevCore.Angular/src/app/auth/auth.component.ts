import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router, ActivatedRoute } from "@angular/router";
import { Location } from '@angular/common';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  pass: string;

  constructor(private authService : AuthService,private router: Router,
    private route: ActivatedRoute, private location: Location) { }

  ngOnInit() {
  }

  authorize(){
    let url = this.route.snapshot.queryParamMap.get('backUrl') || '/';
    this.authService.validatePass(this.pass).subscribe(res=>
      this.router.navigateByUrl(url)      
    );
  }
}