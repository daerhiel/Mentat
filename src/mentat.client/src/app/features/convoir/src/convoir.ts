import { Component, inject, signal } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { of } from 'rxjs';

import { ExplorerService } from '@features/explorer';

@Component({
  selector: 'app-convoir',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './convoir.html',
  styleUrl: './convoir.scss'
})
export class Convoir {
  readonly #explorer = inject(ExplorerService);

  readonly _input = signal<string | null>('Újlipótvárosban lakott egy fiatal, könyvimádó építész, aki minden reggel a Duna-parton sétált.');

  readonly _text = signal<string | null>(null);
  readonly _tokens = rxResource({
    params: this._text,
    stream: ({ params: text }) => text ?
      this.#explorer.lexicon.getSyntax(text) :
      of([]),
    defaultValue: []
  })

  readonly _analyze = () => {
    this._text.set(this._input());
    this._input.set(null);
  };
}
