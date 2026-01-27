import { Component, EventEmitter, Input, Output } from '@angular/core';

export interface ToggleOption {
  label: string;
  value: string;
}

@Component({
  selector: 'app-toggle',
  templateUrl: './toggle.component.html',
  styleUrls: ['./toggle.component.scss'],
})
export class ToggleComponent {
  @Input() options: ToggleOption[] = [];
  @Input() value: string = '';
  @Output() valueChange = new EventEmitter<string>();

  select(option: ToggleOption) {
    if (this.value !== option.value) {
      this.value = option.value;
      this.valueChange.emit(this.value);
    }
  }
}
