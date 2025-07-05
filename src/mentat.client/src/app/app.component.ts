import { Component, inject, signal } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { AsyncPipe } from '@angular/common';
import { RouterOutlet } from '@angular/router';

import { ExplorerService } from './features/explorer';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly #explorer = inject(ExplorerService);

  title = 'mentat.client';

  protected readonly _word = signal("Újlipótváros");

  protected readonly _lexeme = rxResource({
    request: this._word,
    loader: ({ request }) => this.#explorer.lexicon.getLexeme(request)
  });
}
