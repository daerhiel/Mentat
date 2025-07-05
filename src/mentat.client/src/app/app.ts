import { Component, inject, signal } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatToolbar } from '@angular/material/toolbar';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

import { Theme, ThemeManager } from '@core/theme';
import { ExplorerService } from './features/explorer';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet, RouterLink,
    MatToolbar,
    MatMenu, MatMenuItem, MatMenuTrigger,
    MatButton, MatIconButton, MatIcon
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly _themes = inject(ThemeManager);
  readonly #explorer = inject(ExplorerService);

  title = 'mentat.client';

  protected readonly _word = signal("Újlipótváros");

  protected readonly _lexeme = rxResource({
    params: this._word,
    stream: ({ params }) => this.#explorer.lexicon.getLexeme(params)
  });

  /**
   * Gets an active icon for the menu option.
   * @param theme Theme to get the icon for.
   * @returns The icon name.
   */
  protected _getThemeIcon(theme: Theme): string {
    return this._themes.current().id === theme.id ? 'radio_button_checked' : 'radio_button_unchecked'
  }
}
