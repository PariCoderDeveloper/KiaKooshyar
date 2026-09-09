import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CaptchaService } from '../../../core/services/captcha.service';

@Component({
  imports: [
    FormsModule,
  ],
  selector: 'app-captcha',
  styleUrl: './capcha.css',
  templateUrl: './capcha.html',
  standalone:true
})
export class Capcha implements OnInit {
  imageBase64 = '';
  captchaId = '';
  userInput = '';

  @Output() captchaChange = new EventEmitter<{ captchaId: string; code: string }>();

  constructor(private captchaService: CaptchaService) {}

  ngOnInit(): void {
    this.refresh();
  }

  refresh(): void {
    this.captchaService.generate().subscribe(result => {
      this.captchaId = result.captchaId;
      this.imageBase64 = result.imageBase64;
      this.userInput = '';
      this.onValueChange();
    },
    );
  }

  onValueChange(): void {
    this.captchaChange.emit({ captchaId: this.captchaId, code: this.userInput });
  }

}
