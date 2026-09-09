import { Component, inject, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { SignalRService } from './core/services/signalr.service';
import { AuthService } from './core/services/auth.service';

@Component({
  imports: [RouterOutlet],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('kiakooshyarApp');
  signalRService = inject(SignalRService);
  authService = inject(AuthService);
  router = inject(Router)
  ngOnInit(): void {
    this.signalRService.notification$.subscribe(reason =>{
      alert(reason);
      this.authService.logout();
      this.router.navigate(["/login"]);    
    });
      this.signalRService.startConnection();
  }
}
