import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-success-page',
  templateUrl: './success-page.component.html',
  styleUrls: ['./success-page.component.css']
})
export class SuccessPageComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
    // Check if the user is authenticated
    if (!this.authService.isAuthenticatedUser()) {
      // If not authenticated, redirect to the login page
      // This is a backup redirect in case the AuthGuard did not trigger
      this.authService.setAuthenticated(false);
      window.location.href = '/login'; // Use window.location.href to force navigation
    }
  }

}
